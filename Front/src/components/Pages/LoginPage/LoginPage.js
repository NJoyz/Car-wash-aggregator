import React, { useState } from "react";
import cn from "classnames";
import { Form, Field } from "react-final-form";
import routes from "../../../helpers/routes";
import { useDispatch, useSelector } from "react-redux";
import { setSession } from "../../../state/session";
import { selectConstants } from "../../../state/constants";
import { ROLES_OPTIONS } from "../../../constants/ROLES";
import sources from "../../../helpers/sources";
import api from "../../../lib/api";
import { setUserCookie } from "../../../lib/cookie";
import {
  composeValidators,
  required,
  validEmail,
  minValue,
} from "../../../helpers/validations";
import { toast } from "react-toastify";

import Button from "../../Button/Button";
import Select from "../../Select/Select";
import Input from "../../Input/Input";
import Header from "../../Header/Header";
import styles from "./LoginPage.module.scss";

const LoginPage = ({ history }) => {
  const [tab, setTab] = useState(history.location.pathname);

  const constants = useSelector(selectConstants);
  const dispatch = useDispatch();

  const setLoginTab = () => {
    setTab(routes.login);
    history.replace(routes.login);
  };

  const setRegisterTab = () => {
    setTab(routes.register);
    history.replace(routes.register);
  };

  const login = (data) => {
    api
      .post(sources.login, { ...data })
      .then((response) => {
        dispatch(
          setSession({
            ...response.data.user,
            token: response.data.accessToken,
          })
        );
        setUserCookie(response.data.refreshToken);

        history.push(routes.root);
      })
      .catch((err) => {
        toast.success(err.response.data.detail);
      });
  };

  const register = (data) => {
    api
      .post(sources.register, {
        ...data,
        role: data.role.id,
        city: data.city ? data.city.name : null,
      })
      .then((response) => {
        if (response.status === 200) {
          toast.success("Вы зарегестрированы");
          setTimeout(() => {
            login({ email: data.email, password: data.password });
          }, 1000);
        }
      })
      .catch((err) => toast.error(err.response.data.detail));
  };

  const loginBtnCn = cn(styles.containerBoxBtn, {
    [styles.containerBoxBtnActive]: tab === routes.login,
  });
  const registerBtnCn = cn(styles.containerBoxBtn, {
    [styles.containerBoxBtnActive]: tab === routes.register,
  });
  return (
    <div className={styles.login}>
      <Header />
      <div className={styles.wrap}>
        <div className={styles.header}>
          <div className={styles.headerContaner}>
            <div className={styles.containerBox}>
              <Button
                className={loginBtnCn}
                onClick={setLoginTab}
                size="maxWidth"
              >
                Личный кабинет
              </Button>
            </div>
            <div className={styles.containerBox}>
              <Button
                className={registerBtnCn}
                onClick={setRegisterTab}
                size="maxWidth"
              >
                Регистрация
              </Button>
            </div>
          </div>
        </div>
        <div className={styles.loginForm}>
          {tab === routes.login && (
            <Form
              onSubmit={login}
              render={({ handleSubmit }) => (
                <form onSubmit={handleSubmit}>
                  <div className={styles.inputs}>
                    <div className={cn(styles.inner, styles.mobileInner)}>
                      <Field
                        name="email"
                        validate={composeValidators(required, validEmail)}
                        render={({ input, meta }) => (
                          <Input
                            className={styles.input}
                            placeholder="Username or email"
                            meta={meta}
                            {...input}
                          />
                        )}
                      />
                    </div>

                    <div className={cn(styles.inner, styles.mobileInner)}>
                      <Field
                        name="password"
                        type="password"
                        validate={composeValidators(required, minValue(6))}
                        render={({ input, meta }) => (
                          <Input
                            className={styles.input}
                            placeholder="Password"
                            meta={meta}
                            {...input}
                          />
                        )}
                      />
                    </div>
                  </div>
                  <Button
                    className={styles.sigInBtn}
                    type="submit"
                    size="maxWidth"
                    increased
                  >
                    Войти
                  </Button>
                </form>
              )}
            />
          )}
          {tab === routes.register && (
            <Form
              onSubmit={register}
              validate={(values) => {
                const errors = {};
                if (
                  values.password &&
                  values.confirm_password &&
                  values.password !== values.confirm_password
                ) {
                  errors.password = "Пароли не совпадают";
                  errors.confirm_password = "Пароли не совпадают";
                }

                return errors;
              }}
              render={({ handleSubmit }) => (
                <form onSubmit={handleSubmit}>
                  <div className={styles.field}>
                    <Field
                      name="email"
                      type="email"
                      validate={composeValidators(required, validEmail)}
                    >
                      {({ input, meta }) => (
                        <Input
                          className={styles.input}
                          placeholder="Ваш Email *"
                          meta={meta}
                          {...input}
                        />
                      )}
                    </Field>
                  </div>
                  <div className={styles.inputs}>
                    <div className={cn(styles.inner, styles.mobileInner)}>
                      <Field
                        name="firstName"
                        type="firstName"
                        validate={required}
                      >
                        {({ input, meta }) => (
                          <Input
                            className={styles.input}
                            placeholder="Имя *"
                            meta={meta}
                            {...input}
                          />
                        )}
                      </Field>
                    </div>
                    <div className={cn(styles.inner, styles.mobileInner)}>
                      <Field
                        name="lastName"
                        type="lastName"
                        validate={required}
                      >
                        {({ input, meta }) => (
                          <Input
                            className={styles.input}
                            placeholder="Фамилия *"
                            meta={meta}
                            {...input}
                          />
                        )}
                      </Field>
                    </div>
                  </div>
                  <div className={styles.field}>
                    <Field
                      name="password"
                      type="password"
                      validate={composeValidators(required, minValue(6))}
                    >
                      {({ input, meta }) => (
                        <Input
                          className={styles.input}
                          placeholder="Пароль *"
                          meta={meta}
                          {...input}
                        />
                      )}
                    </Field>
                  </div>
                  <div className={styles.field}>
                    <Field
                      name="confirm_password"
                      type="password"
                      validate={required}
                    >
                      {({ input, meta }) => (
                        <Input
                          className={styles.input}
                          placeholder="Поторите пароль *"
                          meta={meta}
                          {...input}
                        />
                      )}
                    </Field>
                  </div>
                  <div className={styles.inputs}>
                    <div className={cn(styles.inner, styles.mobileInner)}>
                      <Field name="role" validate={required}>
                        {({ input, meta }) => (
                          <Select
                            options={ROLES_OPTIONS}
                            placeholder="Выбирите метод регистрации *"
                            meta={meta}
                            {...input}
                          />
                        )}
                      </Field>
                    </div>
                    <div className={cn(styles.inner, styles.mobileInner)}>
                      <Field name="city">
                        {({ input, meta }) => (
                          <Select
                            options={constants.cities}
                            placeholder="Выбирите город"
                            meta={meta}
                            {...input}
                          />
                        )}
                      </Field>
                    </div>
                  </div>
                  <div className={styles.field}>
                    <Field name="phone">
                      {({ input, meta }) => (
                        <Input
                          placeholder="Номер телефона "
                          meta={meta}
                          {...input}
                        />
                      )}
                    </Field>
                  </div>
                  <Button
                    className={styles.sigInBtn}
                    type="submit"
                    size="maxWidth"
                    increased
                  >
                    Зарегистрироваться
                  </Button>
                </form>
              )}
            />
          )}
        </div>
      </div>
    </div>
  );
};

export default LoginPage;

import React from "react";
import { Form, Field } from "react-final-form";
import { required } from "../../helpers/validations";
import { useSelector } from "react-redux";
import { selectConstants } from "../../state/constants";
import api from "../../lib/api";
import sources from "../../helpers/sources";
import { selectSession } from "../../state/session";
import { toast } from "react-toastify";

import Input from "../Input/Input";
import Button from "../Button/Button";
import Select from "../Select/Select";
import styles from "./AddCarWash.module.scss";
import AddPhoto from "../AddPhoto/AddPhoto";
import { CATEGORIES_OPTIONS } from "../../constants/CAR-CATEGORIES";

const AddCarWash = ({ getCarWashList, onClose }) => {
  const session = useSelector(selectSession);

  const saveCarWash = (values) => {
    let arr = [];
    values.carCategories.map((i) => {
      arr.push(i.id);
      return i;
    });

    api
      .post(sources.carWashAdd, {
        ...values,
        partnerId: session.id,
        carCategories: arr,
        image: values.photo && values.photo.data_url,
        cityId: values.cityId.id,
        price: "0",
      })
      .then((response) => {
        if (response.status === 200) {
          toast.success("Мойка добавлена");
          getCarWashList();
          onClose();
        }
      })
      .catch((err) => toast.error("Error save car wash"));
  };
  let currentForm = null;

  const constants = useSelector(selectConstants);

  const formChange = (image) => {
    currentForm.change("photo", image);
  };

  return (
    <div className={styles.contain}>
      <div className={styles.title}>Заполните форму</div>
      <Form
        onSubmit={saveCarWash}
        render={({ handleSubmit, form }) => {
          currentForm = form;
          return (
            <form onSubmit={handleSubmit}>
              <div className={styles.inner}>
                <Field
                  name="name"
                  validate={required}
                  render={({ input, meta }) => {
                    return (
                      <Input
                        placeholder="Название Мойки*"
                        className={styles.input}
                        meta={meta}
                        {...input}
                      />
                    );
                  }}
                />
              </div>
              <div className={styles.inner}>
                <Field
                  name="cityId"
                  validate={required}
                  render={({ input, meta }) => {
                    return (
                      <Select
                        placeholder="Город*"
                        options={constants.cities}
                        meta={meta}
                        {...input}
                      />
                    );
                  }}
                />
              </div>
              <div className={styles.inner}>
                <Field
                  name="address"
                  validate={required}
                  render={({ input, meta }) => {
                    return (
                      <Input
                        placeholder="Адресс*"
                        className={styles.input}
                        meta={meta}
                        {...input}
                      />
                    );
                  }}
                />
              </div>
              <div className={styles.inner}>
                <Field
                  name="carCategories"
                  render={({ input, meta }) => {
                    return (
                      <Select
                        placeholder="Категории автомобилей*"
                        options={CATEGORIES_OPTIONS}
                        isMulti
                        meta={meta}
                        {...input}
                      />
                    );
                  }}
                />
              </div>
              <div className={styles.inner}>
                <Field
                  name="phone"
                  validate={required}
                  render={({ input, meta }) => {
                    return (
                      <Input
                        placeholder="Телефон *"
                        className={styles.input}
                        meta={meta}
                        {...input}
                      />
                    );
                  }}
                />
              </div>
              <div className={styles.inner}>
                <Field
                  name="photo"
                  render={({ input, meta }) => {
                    return (
                      <AddPhoto
                        placeholder="Фото *"
                        formChange={formChange}
                        meta={meta}
                        {...input}
                      />
                    );
                  }}
                />
              </div>
              <Field
                name="description"
                render={({ input, meta }) => (
                  <textarea
                    className={styles.textarea}
                    placeholder="Описание"
                    {...input}
                    meta={meta}
                  />
                )}
              />
              <Button
                type="submit"
                size="maxWidth"
                className={styles.reviewBtn}
              >
                Сохранить мойку
              </Button>
            </form>
          );
        }}
      />
    </div>
  );
};

export default AddCarWash;

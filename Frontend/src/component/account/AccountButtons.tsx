import React from "react";
import Button from "../shared/clickable/Button";
import s from "../shared/container/FlexDisplayed.css";

type AccountButtonsProps = {
    onEditClick: () => void,
    onChangePasswordClick: () => void,
    onDeleteClick: () => void
}

const AccountButtons = ({onEditClick, onChangePasswordClick, onDeleteClick}: AccountButtonsProps) => {
    return (
        <div className={s.flexDisplayed}>
            <Button value='Редактировать' onClick={onEditClick} isDark={true}/>
            <Button value='Изменить пароль' onClick={onChangePasswordClick} isDark={true}/>
            <Button value='Удалить аккаунт' onClick={onDeleteClick} isDark={true}/>
        </div>
    )
}

export default AccountButtons;
import Avatar from '../../elements/avatar/Avatar';
import styles from './main.module.css';

const User = ({name = "Unknown", color = 0, message={text: "Chat has been created", time: "100"}}) => {
    return (
        <div className={styles.user}>
            <Avatar name="Astynate" color={color} />
            <div className={styles.text}>
                <span>{name}</span>
                <span>{message.text}</span>
            </div>
        </div>
    );
}

export default User;
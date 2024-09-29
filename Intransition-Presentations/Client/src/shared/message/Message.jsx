import Avatar from '../../elements/avatar/Avatar';
import styles from './main.module.css';

const Message = ({user}) => {
    return (
        <div className={styles.message}>
            <div className={styles.userAvatar}>
                <Avatar 
                    size='30px'
                    font='18px'
                />
            </div>
            <div className={styles.messageText}>
                <span>Dasdasd</span>
            </div>
        </div>
    );
}

export default Message;
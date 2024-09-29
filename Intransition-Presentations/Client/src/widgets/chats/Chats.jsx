import styles from './main.module.css';
import logo from './images/itransition_logo.png';
import write from './images/write.png';
import User from '../../shared/user/User';

const Chats = () => {
    return (
        <div className={styles.chats}>
            <div className={styles.header}>
                <div className={styles.top}>
                    <div className={styles.name}>
                        <img src={logo} className={styles.logo} />
                        <h1>Messenger</h1>
                    </div>
                    <div className={styles.button}>
                        <img src={write} />
                    </div>
                </div>
                <div className={styles.search}>
                    <input placeholder='Search' />
                </div>
            </div>
            <div className={styles.users}>
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
                <User />
            </div>
        </div>
    );
}

export default Chats;
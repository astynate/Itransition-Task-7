import Chat from "../widgets/chat/Chat";
import Chats from "../widgets/chats/Chats";
import styles from './main.module.css';
import './main.css';

const Layout = () => {
    return (
        <div className={styles.wrapper}>
            <Chats />
            <Chat />
        </div>
    );
}

export default Layout;
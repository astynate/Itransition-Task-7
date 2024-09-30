import styles from './main.module.css';
import logo from './images/itransition_logo.png';
import write from './images/write.png';
import Search from '../../shared/search/Search';
import CreateChat from '../create-chat/CreateChat';
import { useState } from 'react';

const Chats = () => {
    const [isCreateChatOpen, setCreateChatState] = useState(false);

    return (
        <div className={styles.chats}>
            {isCreateChatOpen && 
                <CreateChat close={() => setCreateChatState(false)} />}
            <div className={styles.header}>
                <div className={styles.top}>
                    <div className={styles.name}>
                        <img src={logo} className={styles.logo} />
                        <h1>Messenger</h1>
                    </div>
                    <div className={styles.button} onClick={() => setCreateChatState(true)}>
                        <img src={write} />
                    </div>
                </div>
                <Search />
            </div>
            <div className={styles.users}>
            </div>
        </div>
    );
}

export default Chats;
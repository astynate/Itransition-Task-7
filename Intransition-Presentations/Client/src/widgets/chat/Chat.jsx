import React, { useEffect, useState } from 'react';
import Avatar from '../../elements/avatar/Avatar';
import styles from './main.module.css';
import Message from '../../shared/message/Message';
import arrow from './images/arrow.png';
import plus from './images/plus.png';
import remove from './images/remove.png';

const Chat = ({user = { username: "Unknown", color: 0 }}) => {
    const [text, setText] = useState('');
    const [files, setFiles] = useState([]);
    const textAreaRef = React.createRef();
    const chat = React.createRef();

    const handleChange = (event) => {
        setText(event.target.value);
    };

    const handleKeyDown = (event) => {
      if (event.key === 'Enter' && !event.shiftKey) {
        event.preventDefault();
        sendMessageAsync();
      }
    };

    useEffect(() => {
        textAreaRef.current.style.height = 'inherit';
        const scrollHeight = textAreaRef.current.scrollHeight;
        chat.current.style.setProperty('--height', `${scrollHeight > 300 ? 300 : scrollHeight}px`);
        textAreaRef.current.style.height = scrollHeight > 50 ? scrollHeight + "px" : 'inherit';
    }, [text]);

    const sendMessageAsync = () => {
        setFiles([]);
        setText('');
    }

    return (
        <div className={styles.chat} ref={chat}>
            <div className={styles.header}>
                <div className={styles.left}>
                    <Avatar 
                        name={user.username} 
                        color={user.color} 
                        size='40px'
                        font='18px'
                    />
                    <div className={styles.userData}>
                        <span>{user.username}</span>
                        <span>last seen recently</span>
                    </div>
                </div>
                <div>

                </div>
            </div>
            <div className={styles.content}>
            </div>
            <div className={styles.inputWrapper}>
                <div className={styles.files}>
                    {files.map((element, index) => {
                        const handleRemove = () => {
                            const updatedFiles = files.filter((_, i) => i !== index);
                            setFiles(updatedFiles);
                        };

                        return (
                            <div className={styles.file} key={index}>
                                <span>{element.name}</span>
                                <div className={styles.remove} onClick={handleRemove}>
                                    <img src={remove} draggable="false" />
                                </div>
                            </div>
                        );
                    })}
                </div>
                <div className={styles.input}>
                    <div className={styles.button}>
                        <img src={plus} draggable="false" />
                        <input
                            type='file'
                            multiple
                            onChange={(event) => {
                                const files = event.target.files;
                                
                                if (files.length > 0) {
                                    setFiles(Array.from(files).slice(0, 5));
                                }
                            }} 
                        />
                    </div>
                    <textarea 
                        placeholder='Write a message...' 
                        ref={textAreaRef}
                        rows={1}
                        value={text}
                        onChange={handleChange}
                        onKeyDown={handleKeyDown}
                        maxLength={4000}
                        autoFocus
                    />
                    <div className={styles.button}>
                        <img src={arrow} draggable="false" />
                    </div>
                </div>
                {files.length > 0 && <div className={styles.close} onClick={() => setFiles([])}>
                    <img src={remove} draggable="false" />
                </div>}
            </div>
        </div>
    );
}

export default Chat;
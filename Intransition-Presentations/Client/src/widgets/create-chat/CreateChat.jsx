import { useEffect, useState } from 'react';
import Avatar from '../../elements/avatar/Avatar';
import Search from '../../shared/search/Search';
import styles from './main.module.css';
import remove from './remove.png';

const CreateChat = ({close = () => {}}) => {
    const [users, setUsers] = useState([]);

    const FetchUsers = async (prefix) => {
        await fetch(`/api/users?prefix=${prefix}`)
            .then(response =>{
                return response.json();
            })
            .then(response => {
                setUsers(response ?? []);
            });
    }

    return (
        <div className={styles.wrapper}>
            <div className={styles.window}>
                <div className={styles.header}>
                    <span>New message</span>
                    <div className={styles.exit} onClick={close}>
                        <img src={remove} draggable="false" />
                    </div>
                </div>
                <Search changeInput={FetchUsers} />
                <div className={styles.users}>
                    {users && users.map && users.map(user => {
                        return (
                            <div className={styles.user}>
                                <Avatar 
                                    size='45px' 
                                    font='21px' 
                                    name={user.username} 
                                    color={user.color} 
                                />
                                <span>{user.username}</span>
                            </div>
                        )
                    })}
                </div>
            </div>
        </div>
    );
}

export default CreateChat;
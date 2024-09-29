import React, { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import Login from './elements/login/Login';
import { observer } from 'mobx-react-lite';
import userState from './state/userState';
import Layout from './layout/Layout';

const App = observer(() => {
    const [isLoggedIn, setLogginState] = useState(true);

    useEffect(() => {
        const username = localStorage.getItem('username');
        const color = localStorage.getItem('color');

        setLogginState(!!username && !!color);
        userState.SetUserData(username, color);
    }, [userState.username]);

    return (
        <>
            {isLoggedIn === false && <Login />}
            <Routes>
                <Route path="/" element={<Layout />} />
                <Route path="*" element={<h1>404 - Not Found</h1>} />
            </Routes>
        </>
    );
});

export default App;
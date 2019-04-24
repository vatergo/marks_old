import React, { Component } from 'react';
import './reg.css';

export class Reg extends Component {
    constructor(props) {
        super(props);
        this.signIn = this.signIn.bind(this);
        this.signUp = this.signUp.bind(this);
    }

    signIn() {
        let login = document.querySelector('.login').value;
        let pass = document.querySelector('.password').value;
        fetch('/api/user/Auth', {
            method: 'POST',
            body: JSON.stringify({
                Login: login,
                Password: pass
            }), headers: { 'content-type': 'application/json' }
        })
            .then(function (response) { if (response.status !== 201) throw new Error(response.status); document.cookie = `userName=${login}`; window.location.replace("https://localhost:5001/"); })
            .catch(function (error) { alert('Неверный логин или пароль ' + error.message) });

    }

    signUp() {
        let login = document.querySelector('.login').value;
        let pass = document.querySelector('.password').value;
        fetch('/api/user/Reg', {
            method: 'POST', body: JSON.stringify({
                Login: login,
                Password: pass
            }), headers: { 'content-type': 'application/json' }
        })
            .then(function (response) { if (response.status !== 201) throw new Error(response.status); alert('Вы успешно зарегистрированы'); })
            .catch(function (error) { alert('Пользователь с таким именем уже зарегистрирован ' + error.message) });
    }

    render() {
        return (
            <div>
                <h1>Registration</h1>
                <div className="form">
                    <input type="text" className="login" placeholder="Login" />
                    <input type="password" className="password" placeholder="Password" />
                    <div className="buttons">
                        <button className="login" type='button' onClick={this.signIn}>Sign in</button>
                        <button className="signup"type='button' onClick={this.signUp}>Sign up</button>
                    </div>
                </div>
            </div>
        );
    }
}
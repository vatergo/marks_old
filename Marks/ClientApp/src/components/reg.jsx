import React, { Component } from 'react';

export class Reg extends Component {
    constructor(props) {
        super(props);
        this.onSubmit = this.onSubmit.bind(this);
    }
    onSubmit(event) {
        let login = 'Получить значение из инупутов';
        let pass = document.querySelector('.Password').textContent;
        fetch('/api/user', {
            method: 'POST', body: JSON.stringify({
                Login: login,
                Password: pass
            }), headers: { 'content-type': 'application/json' }
        });
        alert('Вы успешно зарегистрировались');
    }
    render() {
        return (
            <div>
                <h1>Registration</h1>
                <form onSubmit={this.onSubmit} >
                    <input className="Login" placeholder="Login" />
                    <input className="Password" placeholder="Password" />
                    <button type='submit'>Регистрация</button>
                </form>
            </div>
        );
    }
}
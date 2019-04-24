import React, { Component } from 'react';
import './AddProduct.css';

export class AddProduct extends Component {
    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);
    }

    getCookie() {
        let matches = document.cookie.match(new RegExp(
            "(?:^|; )" + 'userName'.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        if (matches) {
            return decodeURIComponent(matches[1]);
        }
        return undefined;
    }
    
    onClick() {
        let value;
        try {
            value = document.querySelector('.url').value.split('/')[5].split('?')[0];
            document.querySelector('.url').value = '';
        } catch (e) {

        }
        let context = this;
        let headers = new Headers();
        headers.append('content-type', 'application/json');
        headers.append('Cookie', `userName=${this.getCookie()}`);
        fetch(`/api/things/${value}`, {
            method: 'GET',
            headers,
        })
            .then(function (response) { if (response.status !== 201) throw new Error(response.status); alert('Продукт добавлен'); context.props.onClick(); })
            .catch(function (error) { alert('Что-то пошло не так ' + error.message) });

    }

    render() {
        return (
            <div>
                <div className="form">
                    <input type="text" className="url" />
                    <button type='button' onClick={this.onClick}>Add</button>   
                </div>
            </div>
        );
    }
}
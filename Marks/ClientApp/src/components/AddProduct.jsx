import React, { Component } from 'react';
import './AddProduct.css';

export class AddProduct extends Component {
    constructor(props) {
        super(props);
        this.onClick = this.onClick.bind(this);
    }

    onClick() {
        let url = document.querySelector('.url').value;
        fetch(`/api/things/${value}`, {
            method: 'GET',
            headers: { 'content-type': 'application/json' }
        })
            .then(function (response) { if (response.status !== 201) throw new Error(response.status); alert('Продукт добавлен'); })
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
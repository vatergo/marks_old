import React, { Component } from 'react';
import { AddProduct } from './components/AddProduct';

export class ProductList extends Component {
    getProducts() {
        fetch(`/api/things/GetAllThings`, {
            method: 'POST',
            headers: { 'content-type': 'application/json' }
        })
            .then(function (response) { if (response.status !== 200) throw new Error(response.status); alert('Продукт добавлен'); })
            .catch(function (error) { alert('Что-то пошло не так ' + error.message) });
    }
    render() {
        return (
            <div>
                <AddProduct />
            </div>
        )
    };
}
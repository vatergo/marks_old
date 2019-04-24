import React, { Component } from 'react';
import { AddProduct } from './AddProduct';
import './ProductList.css';

export class ProductList extends Component {
    constructor(props) {
        super(props);
        this.getProducts = this.getProducts.bind(this);
        this.state = {
            products: [],
        }
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

    getProducts() {
        let context = this;
        let headers = new Headers();
        headers.append('content-type', 'application/json');
        headers.append('Cookie', `userName=${this.getCookie()}`);
        fetch(`/api/things/GetAllThings`, {
            method: 'GET',
            headers,
        })
            .then(function (response) {
                if (response.status !== 200)
                    throw new Error(response.status);
                return response.json();
            })
            .then(function (data) { context.setState({ products: data, }); })
            .catch(function (error) { alert('Что-то пошло не так ' + error.message) });
        
    }

    componentWillMount() {
        this.getProducts();
    }

    render() {
        return (
            <div className="product-list">
                <AddProduct onClick={this.getProducts} />
                <div>
                    <ul>{this.state.products.map((obj) => <li>
                        <div className="thing">
                                <img src={obj.image} alt={obj.image} />
                                <span>{obj.title}</span>
                            </div>
                        </li>)}
                    </ul>
                </div>      
            </div>
        )
    };
}
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

    getProducts() {
        let context = this;
        fetch(`/api/things/GetAllThings`, {
            method: 'GET',
            headers: { 'content-type': 'application/json' }
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
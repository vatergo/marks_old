import React, { Component } from 'react';
import './AddProduct.css';

export class NewProduct extends Component {

    render() {
        return (
            <div>
                <h1>Goods adding</h1>
                <div className="form">
                    
                    <input type="url" className="url"  />
                    <div className="buttons">
                        <button type='button' onClick={this.signIn}>Add Product</button>
                        
                    </div>
                </div>
            </div>
        );

    }
}
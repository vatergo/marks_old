import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import { Reg } from './components/reg'

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            {getCookie('userName') && <Route exact path='/' component={Home} />}
            {getCookie('userName') && <Route path='/counter' component={Counter} />}
            {getCookie('userName') && <Route path='/fetch-data' component={FetchData} />}
            {!getCookie('userName') && <Route path='/auth' component={Reg} />}
      </Layout>
    );
  }
}

import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';


export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
      };
      this.out = this.out.bind(this);
      this.getCookie = this.getCookie.bind(this);
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

    out() {
        document.cookie = 'userName=';// так не затираются куки, нужно что-то другое
        window.location.replace("https://localhost:5001/");
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

  render () {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">Marks</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                <ul className="navbar-nav flex-grow">
                    {this.getCookie() && < NavItem > <NavLink tag={Link} className="text-dark" to="/">Home</NavLink></NavItem>}
                    {this.getCookie() && <NavItem><NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink></NavItem>}
                    {this.getCookie() && <NavItem><NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink></NavItem>}
                    {this.getCookie() && <NavItem><NavLink tag={Link} className="text-dark" to="/products">Products</NavLink></NavItem>}
                    {!this.getCookie() && <NavItem><NavLink tag={Link} className="text-dark" to="/auth">Sign in/Sign up</NavLink></NavItem>}
                    {this.getCookie() && <NavItem><NavLink onClick={this.out}>Out</NavLink></NavItem>}
                </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

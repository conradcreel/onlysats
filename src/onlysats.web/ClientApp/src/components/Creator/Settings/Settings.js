import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import '../../NavMenu.css';

export class Settings extends Component {
  static displayName = Settings.name;

  render() {
    return (
      <div>
        <h2>Settings</h2>

        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/settings/profile">Profile</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/settings/account">Account</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/settings/chat">Chat</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/settings/notifications">Notifications</NavLink>
              </NavItem>
            </ul>
          </Container>
        </Navbar>
      </div>
    );
  }
}

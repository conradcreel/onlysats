import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Settings } from './components/Creator/Settings/Settings';
import { ProfileSettings } from './components/Creator/Settings/ProfileSettings';
import { AccountSettings } from './components/Creator/Settings/AccountSettings';
import { ChatSettings } from './components/Creator/Settings/ChatSettings';
import { NotificationSettings } from './components/Creator/Settings/NotificationSettings';
import { SecuritySettings } from './components/Creator/Settings/SecuritySettings';
import { MessageDetail } from './components/Chat/MessageDetail';
import { MessageList } from './components/Chat/MessageList';
import { Feed } from './components/Creator/Public/Feed';
import { Profile } from './components/Creator/Public/Profile';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/settings' component={Settings} />
        <Route path='/settings/profile' component={ProfileSettings} />
        <Route path='/settings/account' component={AccountSettings} />
        <Route path='/settings/chat' component={ChatSettings} />
        <Route path='/settings/notifications' component={NotificationSettings} />
        <Route path='/settings/security' component={SecuritySettings} />
        <Route path='/chat' component={MessageList} />
        <Route path='/chat/message' component={MessageDetail} />
        <Route path='/profile' component={Profile} />
        <Route path='/feed' component={Feed} />
        {/* TODO: the rest */}
      </Layout>
    );
  }
}

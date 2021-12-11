import React, { Component } from 'react';

export class MessageList extends Component {
  static displayName = MessageList.name;

  render () {
    return (
      <div>
        <h1>Message List</h1>
        <p>TODO: List of all chats, allows you to add a new chat, search, etc.</p>
      </div>
    );
  }
}

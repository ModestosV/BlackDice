import React, { Component } from 'react';

class DownloadItem extends Component {

  render() {

    return (
      <li className="DownloadItem">
        <strong><a href={this.props.download.link}>{this.props.download.title}</a></strong>
      </li>
    );
  }

}

export default DownloadItem;

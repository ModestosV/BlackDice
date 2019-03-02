import React, { Component } from 'react';

class ClickCounter extends Component {
  
  clicknums = 0;
  styleP = {
    display: "inline",
    "padding-left":"1%"
  }

  render() {
    
    this.getClicks()
    return (
      <span>
        <p style={this.styleP}>This link has been click: {this.clicknums}</p>
      </span>
    );
  }

  getClicks() {
    let link = this.props.link;
    link = link.replace('http://', "");

    fetch("/feedback/clicks?link=" + link)
    .then((r) => r.json())
    .then((data) => this.clicknums = data.total_clicks)
  }
}


export default ClickCounter;

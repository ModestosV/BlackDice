import React, { Component } from 'react';

class ClickCounter extends Component {
  
  state= {
    clicks : undefined,
    total_clicks : undefined
  }

  styleP = {
    "display": "inline",
    "padding-left":"1%"
  }

  componentDidMount() {
    this.getClicks();
  }

  render() {
    return (
      <span>
        <p style={this.styleP}>Number of Downloads for this version: {this.state.clicks}</p>
        <p style={this.styleP}>Total Number of Downloads for all versions: {this.state.total_clicks}</p>
      </span>
    );
  }

  getClicks() {
    let link = this.props.link;
    link = link.replace('http://', "");

    fetch("/feedback/clicks?link=" + link)
    .then((r) => r.json())
    .then((data) => {
      this.setState({
        clicks : parseInt(data.total_clicks),
        total_clicks: parseInt(data.total_clicks) +  50 + 23 + 14 + 10
      })
    })
  }
}


export default ClickCounter;

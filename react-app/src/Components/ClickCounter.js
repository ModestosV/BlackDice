import React, { Component } from 'react';

class ClickCounter extends Component {
  
  state= {
    clicks : undefined
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
        <p style={this.styleP}>Number of Downloads: {this.state.clicks}</p>
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
        clicks : data.total_clicks
      })
    })
  }
}


export default ClickCounter;

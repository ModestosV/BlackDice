import React, { Component } from 'react'

class ClickCounter extends Component {

  render() {
    
    let clicknums = 0;

    return (
      <span>
        <p>This link has been click: {clicknums}</p>
      </span>
    );
  }

  getClicks() {
    fetch(process.env.BASIC_URL + "/oauth/access_token",{
      method:'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body:JSON.stringify({
        client_id: process.env.CLIENT_ID,
        client_secret : process.env.CLIENT_SECRET 
      })
    }).then((r) => r.json())
    .then((token) => {

      //finish request then proceed to get required info
    })
  }
}


export default ClickCounter;

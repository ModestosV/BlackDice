import React, { Component } from 'react';
import Downloads from './Components/Downloads'
import './App.css';
import QuoteOfTheDay from './Components/QuoteOfTheDay';

class App extends Component {

  constructor() {
    super()
    this.state = {
      downloads: [],
      quoteOfTheDay: {}
    }
  }

  componentDidMount() {
    this.getDownloads()
    this.getQuoteOfTheDay()
  }

  getQuoteOfTheDay() {
    fetch('https://quotes.rest/qod')
      .then(response => response.json())
      .then(json => {
        this.setState({quoteOfTheDay: json.contents.quotes[0]})
      })
  }
  
  getDownloads() {
    this.setState({downloads: [
      {
        title: "v0.1_Windows_x86",
        link: "https://github.com/ModestosV/BlackDice/releases/download/Iteration4/Online_Grid_Arena_Setup_x86.exe" 
      }
    ]})
  }


  render() {
    return (
      <div className="App">

        <div className="Title">
          <h1>Black Dice</h1>
        </div>

        <div className="Navbar">
          Navbar
        </div>

        <div className="DownloadsHeader">
          <h2>Online Grid Arena Downloads</h2>
        </div>

        <Downloads downloads={this.state.downloads}/>

        <QuoteOfTheDay quote={this.state.quoteOfTheDay}/>

        <div className="Sidebar">
          Sidebar
        </div>

        <div className="Footer">
          Footer
        </div>

      </div> /* .App */
    )
  }

}

export default App;

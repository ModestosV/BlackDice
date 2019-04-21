import React, { Component } from 'react';
import Downloads from './Components/Downloads'
import './App.css';
import QuoteOfTheDay from './Components/QuoteOfTheDay';
import FeedbackLoginPopup from './Components/FeedbackLoginPopup';

class App extends Component {

  constructor() {
    super()
    this.state = {
      downloads: [],
      quoteOfTheDay: {},
      messageFromBackend: "",
      alerts: [],
      inputValueUser: "",
      inputValuePass: "",
      feedback: []
    }
  }

  componentDidMount() {
    this.getDownloads();
    this.getQuoteOfTheDay();
    this.greetBackendAPI();
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
        title: "v1.0_Windows_x86_Installer",
        link: "http://bit.ly/2KTO1F2" 
      }
    ]})
  }

  greetBackendAPI() {
    fetch('/account')
      .then(response => response.json())
      .then(json => {
        this.setState({messageFromBackend: json.message})
      })
  }

  render() {
    var elements = []
    if(this.state.alerts.length > 0) {
      elements.push(
        <div id="Alert">
          {this.state.alerts.pop()}
        </div>
      )  
    }

    return (
      <div className="App" id="App">

        {elements}

        <div className="Title">
          <h1>Black Dice</h1>
        </div>

        <div className="Navbar">
          <FeedbackLoginPopup state={this.state}></FeedbackLoginPopup>
        </div>

        <div className="DownloadsHeader">
          <h2>Offline Grid Arena Downloads</h2>
        </div>
		
        <div className="Downloads">
          <Downloads downloads={this.state.downloads}/>
        </div>

        <QuoteOfTheDay quote={this.state.quoteOfTheDay}/>

        <div className="Footer">
          Footer
        </div>

      </div> /* .App */
    )
  }

}

export default App;

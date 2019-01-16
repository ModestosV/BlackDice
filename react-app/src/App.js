import React, { Component } from 'react';
import REactDom from 'react-dom';
import Popup from 'react-popup';
import Downloads from './Components/Downloads'
import './App.css';
import QuoteOfTheDay from './Components/QuoteOfTheDay';
import TermsOfServicePopUp from './Components/TermsOfServicePopUp'

ReactDom.render(
	<Popup/>,
	document.getElementById('popupConainer')
	);

class App extends Component {

  constructor() {
    super()
    this.state = {
      downloads: [],
      quoteOfTheDay: {},
      messageFromBackend: ""
    }
  }

  componentDidMount() {
    this.getDownloads()
    this.getQuoteOfTheDay()
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
        title: "v0.1_Windows_x86_Installer",
        link: "https://drive.google.com/uc?export=download&id=1PloCi7eWnMWsdmUcarXXao1pBdsjh4II" 
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
          Sidebar<br/><br/>
          {this.state.messageFromBackend}
        </div>

        <div className="Footer">
          Footer
        </div>

      </div> /* .App */
    )
  }

}

export default App;

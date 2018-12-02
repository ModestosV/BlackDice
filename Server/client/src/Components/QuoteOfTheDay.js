import React, { Component } from 'react'

class QuoteOfTheDay extends Component {

  render() {
    
    let quoteOfTheDay

    if (this.props.quote) {
      quoteOfTheDay = this.props.quote
    }

    return (
      <div className="QuoteOfTheDay">
        <h3>Quote of the Day</h3>
        {quoteOfTheDay.quote}<br/>
        <em>– {quoteOfTheDay.author}</em>
        <br/><br/>
      </div>
    );
  }

}

export default QuoteOfTheDay;

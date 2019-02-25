import React, { Component } from 'react'
import { Container, Table } from 'react-bootstrap'
import '../App.css';

class FeedbackView extends Component {
  
  styleContainer = {
    "padding-top" : "1%",
    "padding-left" : "5%",
    "padding-right": "5%"
  }

  styleTable = {
    "width" : "100%"
  }

  styleColumns = {
    "text-align": "center",
    "padding-top": "3%"
  }

  styleColumnLast = {
    "text-align": "center",
    "word-wrap": "break-word",
    "word-break" : "break-all",
    "width" : "60%",
    "padding-top": "3%"
  }
  
  render() {
    var feedbackArray = this.props.feedback;
    var elements = feedbackArray.map((t, index) => {
      return(
        <tr>
          <td style={this.styleColumns}>
            {index+1}
          </td>
          <td style={this.styleColumns}>
            {t.email}
          </td>
          <td style={this.styleColumnLast}>
            {t.message}
          </td>
        </tr>
      )
    })
    return(
      <Container style={this.styleContainer} className="TableFeedback">
        <div>
          <Table style={this.styleTable}>
            <thead>
              <tr>
                <th>#</th>
                <th>Email</th>
                <th>Feedback</th>
              </tr>
            </thead>
            <tbody>
              {elements}
            </tbody>
          </Table>
        </div>
      </Container>
    )
  }
}

export default FeedbackView;
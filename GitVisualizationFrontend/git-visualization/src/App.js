import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import CalendarHeatmap from 'react-calendar-heatmap';
import Popup from 'reactjs-popup';

function App() {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch('/contributions?path=D:/repos/coding-challenge-git-contributions-visualization&numberOfDays=7')
      .then(response => response.json())
      .then(data => setData(data));
  }, []);

  
  let heatmapData = [];
  // let startDate = new Date();
  // let endDate = new Date();
  if (data) {
      let userEmail = 'nemanjamilenkovic@live.com';
      let userContributions = data[userEmail];

      if (userContributions) {
        console.log(userContributions.totalContributions);
        heatmapData = userContributions.totalContributions
        .filter(contribution => contribution.Date)
        .map(contribution => ({
          date: new Date(contribution.Date).toISOString().slice(0, 10),
          count: contribution.numberOfContributions,
        }));
        
      }
        // startDate = new Date(Math.min(...heatmapData.map(d => new Date(d.date))));
        // endDate = new Date(Math.max(...heatmapData.map(d => new Date(d.date))));
    }

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload you fool.
        </p>
        {data && <p>Data from API: {JSON.stringify(data)}</p>}
        {data && <CalendarHeatmap
          startDate={new Date().setFullYear(new Date().getFullYear() - 1)}
          endDate={new Date()}
          values={heatmapData}
          // ... other props
        />}
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;

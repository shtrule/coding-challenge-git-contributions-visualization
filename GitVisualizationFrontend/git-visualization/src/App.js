import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import CalendarHeatmap from 'react-calendar-heatmap';
import Popup from 'reactjs-popup';
import 'react-calendar-heatmap/dist/styles.css';

function App() {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch('/contributions?path=D:/repos/coding-challenge-git-contributions-visualization&numberOfDays=7')
      .then(response => response.json())
      .then(data => {
        console.log('API data:', data);
        setData(data);
      })
      .catch(error => console.error('Error fetching data:', error));
  }, []);

  let heatmapData = [];

  if (data) {
    let userEmail = 'nemanjamilenkovic@live.com';
    let userContributions = data[userEmail];

    if (userContributions && Array.isArray(userContributions.totalContributions)) {
      console.log('User contributions:', userContributions.totalContributions);
      heatmapData = userContributions.totalContributions
        .filter(contribution => contribution.date)
        .map(contribution => ({
          date: new Date(contribution.date).toISOString().slice(0, 10),
          count: contribution.numberOfContributions,
        }));
      console.log('Heatmap data:', heatmapData);
    }
  }

  const getTooltipDataAttrs = (value) => {
    // Temporary hack around null value.date issue
    if (!value || !value.date) {
      return null;
    }
    // Configuration for react-tooltip
    return {
      'data-tip': `${value.date} has count: ${value.count}`,
    };
  };

  const classForValue = (value) => {
    if (!value) {
      return 'color-empty';
    }
    if (value.count > 3) {
      return `color-github-max`;
    }
    return `color-github-${value.count}`;
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload you fool.
        </p>
        {data && <p>Data from API: {JSON.stringify(data)}</p>}
        {data && (
          <CalendarHeatmap
            startDate={new Date(new Date().setFullYear(new Date().getFullYear() - 1))}
            endDate={new Date()}
            values={heatmapData}
            classForValue={classForValue}
            tooltipDataAttrs={getTooltipDataAttrs}
          />
        )}
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

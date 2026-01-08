import { useEffect, useState } from 'react';
import './App.css';

function App() {
  const [forecast, setForecast] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    // In development, CRA proxies "/weatherforecast" to the backend
    fetch('/weatherforecast')
      .then(async (res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}`);
        const data = await res.json();
        setForecast(data);
      })
      .catch((err) => setError(err.message));
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <h1>E-Learning Demo</h1>
        <p>Backend-connected weather forecast:</p>
        {error && <p style={{ color: 'salmon' }}>Error: {error}</p>}
        {!error && forecast.length === 0 && <p>Loading...</p>}
        <ul style={{ listStyle: 'none', padding: 0 }}>
          {forecast.map((item, idx) => (
            <li key={idx} style={{ margin: '8px 0' }}>
              <strong>{item.date}</strong> — {item.summary} —
              <span style={{ marginLeft: 6 }}>{item.temperatureC}°C</span>
              <span style={{ marginLeft: 6 }}>({item.temperatureF}°F)</span>
            </li>
          ))}
        </ul>
      </header>
    </div>
  );
}

export default App;

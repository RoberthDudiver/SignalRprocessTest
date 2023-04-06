import React, { useState, useEffect } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';

function App() {
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5051/retirohub")
      .withAutomaticReconnect()
      .build();

    connection.start()
      .then(() => console.log("Conectado a SignalR"))
      .catch(console.error);

    connection.on("RetiroProcessed", (retiro, status) => {
      console.log(retiro);
      setMessages(prevMessages => [...prevMessages, { retiro, status }]);
    });
  }, []);

  return (
    <div>
      <h1>Messages</h1>
      {messages.map((message, index) => (
        <div key={index}>
          <p>Retiro: {message.retiro.id}</p>
          <p>Status: {message.retiro.estado}</p>
        </div>
      ))}
    </div>
  );
}

export default App;

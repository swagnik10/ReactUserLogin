import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { BrowserRouter } from "react-router-dom";
import { Provider } from 'react-redux';
import { store } from './app/store.ts';
import React from 'react';
import { Toaster } from "sonner";


createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <App />
        <Toaster richColors />
      </BrowserRouter>
    </Provider>
  </React.StrictMode>
)

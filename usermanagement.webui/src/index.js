import React from 'react';
import ReactDOM from 'react-dom/client';
import {RouterProvider} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { appRouter } from './utils/routes';





const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <RouterProvider router={appRouter} />
  </React.StrictMode>
);

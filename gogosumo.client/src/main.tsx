import React from "react"
import ReactDOM from "react-dom/client"
import { BrowserRouter, Route, Routes } from "react-router-dom"
import App from "./App.tsx"
import "./index.css"
import RootLayout from "./components/root-layout.tsx"
import Providers from "./providers.tsx"

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Providers>
                    <Route path="/" element={<App />}>
                        <Route index element={<RootLayout />}/>
                    </Route>
                </Providers>
            </Routes>
        </BrowserRouter>
    </React.StrictMode>
)

import React from "react"
import ReactDOM from "react-dom/client"
import { BrowserRouter, Route, Routes } from "react-router-dom"
import App from "./App.tsx"
import "./index.css"
import RootLayout from "./components/root-layout.tsx"
import Providers from "./providers.tsx"

ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <Providers>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<RootLayout />}>
                        <Route index element={<App />} />
                    </Route>
                </Routes>
            </BrowserRouter>
        </Providers>
    </React.StrictMode>
)

import RootLayout from "./components/layout"
import Providers from "./providers"
import UserTable from "./user-table"

function App() {
    return (
        <Providers>
            <RootLayout>
                <main className="flex bg-background min-h-screen flex-col items-center justify-between p-24">
                    <div className="container mx-auto py-10">
                        <UserTable />
                    </div>
                </main>
            </RootLayout>
        </Providers>
    )
}

export default App

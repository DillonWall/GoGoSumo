import UserTable from "./user-table"

function App() {
    return (
        <main className="flex bg-background min-h-screen flex-col items-center justify-between p-24">
            <div className="container mx-auto py-10">
                <UserTable />
            </div>
        </main>
    )
}

export default App

import { ChevronRight } from "lucide-react";
import { Button } from "./components/ui/button";
import UserTable from "./user-table"

function App() {
    const imageUrl = "https://images.unsplash.com/photo-1493976040374-85c8e12f0c0e?ixlib=rb-4.0.3&auto=format&fit=crop&w=1920&q=80";

    return (
        <main className="flex-grow">
            <section className="relative bg-cover bg-center py-32" style={{ backgroundImage: `url('${imageUrl}')` }}>
                <div className="absolute inset-0 bg-black bg-opacity-50"></div>
                <div className="relative container px-4 text-center text-white">
                    <h1 className="text-4xl md:text-6xl font-bold mb-4">Discover the Beauty of Japan</h1>
                    <p className="text-xl mb-8">Unforgettable tours and dream weddings in the Land of the Rising Sun</p>
                    <Button size="lg" className="bg-red-600 hover:bg-red-700">
                        Start Your Journey
                        <ChevronRight className="ml-2 h-4 w-4" />
                    </Button>
                </div>
            </section>

            <div className="container mx-auto py-10">
                <UserTable />
            </div>
        </main>
    )
}

export default App

import { cn } from "@/lib/utils"
import Header from "../header"
import { Outlet } from "react-router-dom"

const RootLayout = () => {
	return (
		<div className={cn("min-h-screen bg-background font-sans antialiased")}>
			<Header />
			<Outlet />
            <Footer />
		</div>
	)
}
export default RootLayout

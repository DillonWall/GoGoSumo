import { cn } from "@/lib/utils"
import Header from "../header"

const RootLayout = ({ children }: { children: React.ReactNode }) => {
	return (
		<div className={cn("min-h-screen bg-background font-sans antialiased")}>
			<Header />
			{children}
		</div>
	)
}
export default RootLayout

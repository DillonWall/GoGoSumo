import { cn } from "@/lib/utils"
import Header from "./header"

const RootLayout = ({ children }: { children: React.ReactNode }) => {
	return (
		<html lang="en">
			<body className={cn("min-h-screen bg-background font-sans antialiased")}>
				<Header />
				{children}
			</body>
		</html>
	)
}
export default RootLayout

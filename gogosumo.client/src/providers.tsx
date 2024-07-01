import { ThemeProvider } from "@/components/theme-provider"
import { ClerkProvider } from "@clerk/clerk-react"

const publishableKey = import.meta.env.VITE_CLERK_PUBLISHABLE_KEY!

const Providers = ({ children }: { children: React.ReactNode }) => {
	return (
		<>
			<ClerkProvider publishableKey={publishableKey}>
				<ThemeProvider defaultTheme="system" storageKey="vite-ui-theme">
					{children}
				</ThemeProvider>
			</ClerkProvider>
		</>
	)
}
export default Providers

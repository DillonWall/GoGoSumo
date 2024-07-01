import { ThemeProvider } from "@/components/theme-provider"
// import { ClerkProvider } from "@clerk/nextjs"
const Providers = ({ children }: { children: React.ReactNode }) => {
	return (
		<>
			{/* <ClerkProvider> */}
			<ThemeProvider defaultTheme="system" storageKey="vite-ui-theme">
				{children}
			</ThemeProvider>
			{/* </ClerkProvider> */}
		</>
	)
}
export default Providers

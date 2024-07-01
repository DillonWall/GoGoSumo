import { SignInButton, SignedIn, SignedOut, UserButton } from "@clerk/clerk-react"
import RootLayout from "./components/layout"
import { ModeToggle } from "./components/mode-toggle"
import { Button } from "./components/ui/button"
import Providers from "./providers"

function App() {
	return (
		<Providers>
			<RootLayout>
				<main className="flex bg-background min-h-screen flex-col items-center justify-between p-24">
					<Button>Click Me</Button>
					<ModeToggle />
					<SignedIn>
						<UserButton afterSignOutUrl={window.location.href} />
					</SignedIn>
					<SignedOut>
						<SignInButton mode="modal" />
					</SignedOut>
				</main>
			</RootLayout>
		</Providers>
	)
}

export default App

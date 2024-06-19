import { ModeToggle } from "@/components/mode-toggle"
import { Button } from "@/components/ui/button"
import { SignInButton, SignedIn, SignedOut, UserButton } from "@clerk/nextjs"
import Providers from "./providers"

export default function Home() {
	return (
		<main className="flex min-h-screen flex-col items-center justify-between p-24">
			<Providers>
				<SignedOut>
					<SignInButton />
				</SignedOut>
				<SignedIn>
					<UserButton />
				</SignedIn>
				<Button>Click Me</Button>
				<ModeToggle></ModeToggle>
			</Providers>
		</main>
	)
}

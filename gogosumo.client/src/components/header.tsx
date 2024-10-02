import { SignInButton, SignedIn, SignedOut, UserButton, ClerkLoading } from "@clerk/clerk-react"
import { ModeToggle } from "./components/mode-toggle"

const Header = () => {
	return (
		<header className="bg-slate-900">
			<div className="container mx-auto flex justify-between items-center py-4">
				<div className="flex items-center gap-4 text-2xl font-bold">
					<img
						src="/logo.jpeg"
						width={48}
						height={48}
						alt="an image of the face of a sumo wrestler"
						className="rounded"
					/>
					GoGo Sumo
				</div>
				<div className="flex gap-4 items-center">
					<ModeToggle />

					<SignedIn>
						<UserButton afterSignOutUrl={window.location.href} />
					</SignedIn>
					<SignedOut>
						<SignInButton mode="modal" />
					</SignedOut>
					<ClerkLoading>Loading...</ClerkLoading>
				</div>
			</div>
		</header>
	)
}
export default Header

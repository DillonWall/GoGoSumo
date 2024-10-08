import { SignInButton, SignedIn, SignedOut, UserButton, ClerkLoading } from "@clerk/clerk-react"
import { ModeToggle } from "./mode-toggle"

const Header = () => {
    return (
        <header className="bg-orange-100 dark:bg-slate-900">
            <div className="container mx-auto flex justify-between items-center py-4">
                <div className="flex w-1/3 justify-start items-center gap-4 text-2xl font-bold">
                    <img
                        src="/logo.jpeg"
                        width={48}
                        height={48}
                        alt="an image of the face of a sumo wrestler"
                        className="rounded"
                    />
                    GoGo Sumo
                </div>
                <nav className="flex w-1/3 justify-center space-x-4">
                    <a href="#">Home</a>
                    <a href="#">Tours</a>
                    <a href="#">Weddings</a>
                    <a href="#">About</a>
                    <a href="#">Contact</a>
                </nav>
                <div className="flex w-1/3 justify-end gap-4 items-center">
                    <ModeToggle />

                    <SignedIn>
                        <UserButton />
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

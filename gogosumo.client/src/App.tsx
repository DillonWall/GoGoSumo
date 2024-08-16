import RootLayout from "./components/layout"
import Providers from "./providers"
import { DataTable } from "./components/data-table"
import { columns, UserEntity } from "./components/columns"

function getData(): UserEntity[] {
	return [
	  {
		clerk_id: "728ed52f",
		user_phone: "123-444-3131",
		user_fluent_languages: ["en", "jp"],
		role_name: "ticketer",
	  },
	]
}

function App() {
	const data: UserEntity[] = getData();

	return (
		<Providers>
			<RootLayout>
				<main className="flex bg-background min-h-screen flex-col items-center justify-between p-24">
					<div className="container mx-auto py-10">
						<DataTable columns={columns} data={data} />
					</div>
				</main>
			</RootLayout>
		</Providers>
	)
}

export default App

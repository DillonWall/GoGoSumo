import { ColumnDef } from "@tanstack/react-table"

// This type is used to define the shape of our data.
// You can use a Zod schema here if you want.
export type UserEntity = {
	clerk_id: string
	user_phone: string
	user_fluent_languages: string[]
	role_name: string
}

export const columns: ColumnDef<UserEntity>[] = [
	{
		accessorKey: "clerk_id",
		header: "ID",
	},
	{
		accessorKey: "user_phone",
		header: "Phone",
	},
	{
		accessorKey: "user_fluent_languages",
		header: "Fluent Languages",
	},
	{
		accessorKey: "role_name",
		header: "Role Name",
	},
]

import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('b_optional_dependent')
export class B_OptionalDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  optionalDepAttr: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}
import { Entity, Index, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('complex_default_index')
@Index(['fieldA', 'fieldB'])
export class ComplexDefaultIndex {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  fieldA: string;

  @Column()
  fieldB: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}